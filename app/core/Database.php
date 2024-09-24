<?php
define("DB_NAME", "weather"); //ADD YOUR DB NAME
define("DB_PASS", "weather123"); //ADD YOUR DB PASSWORD
define("SERVICE_NAME", "orcl7614"); //ADD YOUR SERVICE NAME


Trait Database {

    public function connect() {
        $db = "(DESCRIPTION =
            (ADDRESS = (PROTOCOL = TCP)(HOST = jnimi)(PORT = 1521))
            (CONNECT_DATA =
            (SERVER = DEDICATED)
            (SERVICE_NAME = " . SERVICE_NAME .")
            )
        )"; //ADD YOUR DATABASE CONNECT

        $conn = oci_connect(DB_NAME, DB_PASS, $db);

        return $conn;

    }

    public function query($query, $data = []) {
        //print_r($query . "\n");

        $stmt = oci_parse($this->connect(), $query);

        if (count($data)) {
            $keys = array_keys($data);
            foreach($keys as $key) {
                oci_bind_by_name($stmt, $key, $data[$key]);
            }
        }

        $check = oci_execute($stmt);
    
        if ($check) {
            if ((substr($query, 0, 6) == "SELECT")) {
                oci_fetch_all($stmt, $res, null, null, OCI_FETCHSTATEMENT_BY_ROW);
                if (is_array($res) && count($res)) {
                    return $res;
                }
            }         
        } else {
            return false;
        }
        
    }

    public function findAll($_table = "users") {
        $query = "SELECT * FROM $_table";
        return $this->query($query);
    }

    public function selectWhere($data, $param = ["*"], $_table = "users") { //SELECT 
        $keys = array_keys($data);
        $query = "SELECT " . implode(", ", $param) . " FROM $_table WHERE ";

        foreach($keys as $key) {
            $query .= strtoupper($key) . " = :" . strtoupper($key) . " AND ";
        }

        $query = trim($query, " AND ");

        return $this->query($query, $data);
    }

    public function insert($data, $_table = "users") { //INSERT INTO $table (data[0], ...) VALUES (data[0],...);
        $keys = array_keys($data);

        foreach($keys as $key) {
            if ($key == "password") {
                $data[$key] = password_hash($data[$key], PASSWORD_DEFAULT);
            }
        }

        $query = "INSERT INTO $_table (" . strtoupper(implode(", ", ($keys))) . ") VALUES (:" . strtoupper(implode(", :", $keys)) . ")";
        $this->query($query, $data);
        return false;
    }

    public function update($data, $c_data, $_table = "users") {
        $keys = array_keys($data);
        $query = "UPDATE $_table SET "; //UPDATE accounts SET
        
        foreach($keys as $key) {
            $query .= strtoupper($key) . " = :" . strtoupper($key) . ", "; // UPDATE accounts SET ACCOUNT_PREFIX = :ACCOUNT_PREFIX
        }

        $query = trim($query, ", ");

        $query .= " WHERE ";

        $c_keys = array_keys($c_data);

        foreach ($c_keys as $c_key) {
            $query .= strtoupper($c_key) .  " = :" . strtoupper($c_key) . " AND ";
            $data[$c_key] = $c_data[$c_key];
        }

        $query = trim($query, " AND ");

        $this->query($query, $data);
        return false;
    }

    public function delete($data, $_table) {
        $keys = array_keys($data);
        $query = "delete FROM $_table WHERE ";

        foreach($keys as $key) {
            $query .= strtoupper($key) . " = :" . strtoupper($key) . " AND ";
        }

        $query = trim($query, " AND ");

        return $this->query($query, $data);

    }

}


?>
