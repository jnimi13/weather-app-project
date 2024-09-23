<?php

class Login extends Controller {
    public function index() {
        $user = new User;
        //We filter the form inputs and add them to an array
        $form_input["username"] = filter_input(INPUT_POST, "username",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        $form_input["password"] = filter_input(INPUT_POST, "password",
                                    FILTER_SANITIZE_SPECIAL_CHARS);
        
        $user->authenticate($form_input);
        $this->view("login");
        array_to_html(obj_to_array($user->get_errors()));
    }

}
