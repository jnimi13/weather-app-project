<?php 

Trait Model {
    use Database;

    protected $limit = 10;
    protected $offset = 0;
    protected $order_type = "DESC";
    protected $errors = [];

    public function get_errors() {
        return $this->errors;
    }

    public function findAll() {
        $query = "SELECT * FROM $this->table";
        return $this->query($query);
    }

    public function where($data, $data_not = []) {
        $keys = array_keys($data);
        $keys_not = array_keys($data_not);
        $query = "SELECT * FROM $this->table WHERE ";

        foreach ($keys as $key) {
            $query .= $key . " = :" . $key . " && ";
        }
        foreach ($keys_not as $key) {
            $query .= $key . " != :" . $key . " && ";
        }

        $query = trim($query, " && ");
        $query .= " limit $this->limit offset $this->offset";
        $data = array_merge($data, $data_not);

        return $this->query($query, $data);
    }

    public function first($data, $data_not = []) {
        $query = "SELECT * FROM $this->table WHERE";
        $keys = array_keys($data);
        $keys_not = array_keys($data_not);

        foreach ($keys as $key) {
            $query .= $key . " = :" . $key . " && ";
        }
        foreach ($keys_not as $key) {
            $query .= $key . " != :" . $key . " && ";
        }

        $query = trim($query, " && ");
        $query .= " limit $this->limit offset $this->offset";
        $data = array_merge($data, $data_not);

        $result = $this->query($query, $data);
        if ($result)
            return $result[0];
        return false;
    }


    public function insert($data) {
        $keys = array_keys($data);
        //This is to hash the password
        foreach($keys as $key) {
            if ($key == "password") {
                $data[$key] = password_hash($data[$key], PASSWORD_DEFAULT);
            }
        }

        $query = "INSERT INTO users (" . implode(", ", $keys) . ") VALUES (:" . implode(", :", $keys) . ")";
        $this->query($query, $data);

        return false;
    }

    public function update($id, $data, $id_column = 'id') {
        
        if (!empty($this->allowed_columns)) {

            foreach ($data as $key => $value) {
                if (!in_array($key, $this->allowed_columns)) {
                    unset($data[$key]);
                }
            }
        }
        
        $keys = array_keys($data);
        $query = "UPDATE $this->table SET ";

        foreach ($keys as $key) {
            $query .= $key . " = :" . $key . ", ";
        }

        $query = trim($query, ", ");

        $query .= " WHERE $id_column = :$id_column";

        $data[$id_column] = $id;
        $this->query($query, $data);
    }

    public function delete($id, $id_column = 'id') {
        $data[$id_column] = $id;
        $query = "DELETE FROM $this->table WHERE $id_column = :$id_column";
        $this->query($query, $data);
        return false;
    }

}