<?php

class Logout extends Controller {
    public function index() {
        session_destroy();
        session_start();
        $this->view("home");
    }

}
