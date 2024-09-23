<?php
class User {
    use Model;
    
    protected $table = "users";
    protected $allowed_columns = [
        "name",
        "age"
    ];
    
    //Validates user inputs
    public function validate_signup($data) {
        $this->errors = [];  

            if (empty($data["username"])) {
                $this->errors["username"] = "Username is required"; 
            }
            if ($this->user_exists($data)) {
                $this->errors["username"] = "Username is already taken"; 
            }
            if (empty($data["password"])) {
                $this->errors["password"] = "Password is required"; 
            }
            if (empty($data["firstname"])) {
                $this->errors["firstname"] = "First Name is required"; 
            }
            if (empty($data["lastname"])) {
                $this->errors["lastname"] = "Last Name is required"; 
            }
            if (empty($data["email"])) {
                $this->errors["email"] = "Email is required"; 
            }

        if (empty($this->errors)) {
            return true;
        } else {
            return false;
        }
    } 

    public function validate_login($data) {
        $this->errors = [];

        if (empty($data["username"])) {
            $this->errors["username"] = "Username is required"; 
        }
        if (empty($data["password"])) {
            $this->errors["password"] = "Password is required"; 
        }

        if (empty($this->errors)) {
            return true;
        } else {
            return false;
        }
    }

    //Checks if the user exists in our table 
    public function user_exists($data) {
        $user_data["username"] = $data["username"];
        $result = $this->selectWhere($user_data);

        if ($result) {
            return true;
        }
        return false;
    }

    //Adds a user to the table
    public function add_user($data) {
        if ($_SERVER["REQUEST_METHOD"] == "POST") {
            if ($this->validate_signup($data) && !$this->user_exists($data)) {
                $this->insert($data);
            }
        }
    }
    //Sets the current user data for the session
    public function setSessionUserInfo($data) {
        $user_data["username"] = $data["username"];
        $user_data = $this->selectWhere($user_data);
        //We convert the $result object to an array
        $user_data = json_decode(json_encode($user_data), true);

        $_SESSION["CURRENT_USER"] = $user_data[0]["USERNAME"];
        $_SESSION["CURRENT_EMAIL"] = $user_data[0]["EMAIL"];
        $_SESSION["CURRENT_FIRST_NAME"] = $user_data[0]["FIRSTNAME"];
        $_SESSION["CURRENT_LAST_NAME"] = $user_data[0]["LASTNAME"];
    }

    //Authenticate user to login
    public function authenticate($data) {
        $this->errors = [];
        if ($_SERVER["REQUEST_METHOD"] == "POST") {
            if ($this->validate_login($data) && $this->user_exists($data)) {
                $user_data["username"] = $data["username"];
                $user_data = $this->selectWhere($user_data);
                print_r($data["password"]);
                
                $user_data = json_decode(json_encode($user_data), true);
            
                if (password_verify($data["password"], $user_data[0]["PASSWORD"])) {
                    $this->setSessionUserInfo($data);
                    redirect("dashboard");
                } else {
                    $this->errors["password"] = "Password is incorrect!";
                } 
            } else if (!empty($data["username"]) && !$this->user_exists($data)) {
                $this->errors["username"] = "This username does not exist";
            }
        }
    }

}