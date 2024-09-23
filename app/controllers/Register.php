<?php

class Register extends Controller {
    public function index() {
        $user = new User;
        //Filter user inputs and adds them to the $form_input array
        $form_input["username"] = filter_input(INPUT_POST, "username",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["password"] = filter_input(INPUT_POST, "password",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["first_name"] = filter_input(INPUT_POST, "first_name",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["last_name"] = filter_input(INPUT_POST, "last_name",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["email"] = filter_input(INPUT_POST, "email",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["email"] = filter_input(INPUT_POST, "email",
                                    FILTER_VALIDATE_EMAIL);

        //Adds user to the table if the inputs are valid
        $user->add_user($form_input);
        $this->view("register");
        array_to_html(obj_to_array($user->get_errors()));
    }

}
