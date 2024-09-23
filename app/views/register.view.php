<?php
    $user = new User;
    //Filter user inputs and adds them to the $form_input array
    $form_input["username"] = filter_input(INPUT_POST, "username",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["password"] = filter_input(INPUT_POST, "password",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["firstname"] = filter_input(INPUT_POST, "firstname",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["lastname"] = filter_input(INPUT_POST, "lastname",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["email"] = filter_input(INPUT_POST, "email",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["email"] = filter_input(INPUT_POST, "email",
                                FILTER_VALIDATE_EMAIL);
    $form_input["phonenumber"] = filter_input(INPUT_POST, "phone",
                                    FILTER_SANITIZE_SPECIAL_CHARS);

    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        if (isset($_POST["register"])) {
            //Adds user to the table if the inputs are valid
            $user->add_user($form_input);
            $new_user["username"] = $form_input["username"];
            //redirect();
        }
    }

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Project</title>
    <link type="text/css" rel="stylesheet" href="<?=ROOT?>/assets/css/styles.css" />
</head>
<body>
    <div class="main-section" id="home-page">
        <div class="form-section home">
            <h2>Register</h2>
            <p>Welcome! Please enter your information to register.</p>
            <form method="POST" id="registration">
                <input type="text" name="username" placeholder="Username"/>
                <br/>
                <input type="password" name="password" placeholder="Password" />
                <br/>
                <input type="text" name="firstname" placeholder="First Name"/>
                <br/>
                <input type="text" name="lastname" placeholder="Last Name" />
                <br/>
                <input type="email" name="email" placeholder="Email" />
                <br/>
                <input type="text" name="phone" placeholder="Phone Number"/>
                <br />
                <div class="flex">
                    <input type="checkbox" name="terms" />
                    <p>Accept our terms and conditions<span class="required">*</span></p>
                </div>
                <input class="btn primary-btn" type="submit" name="register" value="Register" />
            </form>
            <p>Already a User?</p><a href="<?=ROOT?>/login">Login</a>
            <?php array_to_html(obj_to_array($user->get_errors())); ?>
        </div>
    </div>
    <script type="text/javascript" src="<?=ROOT?>/assets/js/image.js"></script>
</body>
</html>