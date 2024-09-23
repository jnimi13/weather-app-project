<?php

    $user = new User;
    //We filter the form inputs and add them to an array
    $form_input["username"] = filter_input(INPUT_POST, "username",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    $form_input["password"] = filter_input(INPUT_POST, "password",
                                FILTER_SANITIZE_SPECIAL_CHARS);
    
    $user->authenticate($form_input);
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
            <h2>Login</h2>
            <p>Welcome back! Please login to your account.</p>
            <form method="POST" id=registration>
                <input type="text" name="username" placeholder="Username" />
                <br/>
                <input type="password" name="password" placeholder="Password" />
                <br/>
                <div class="flex">
                    <input type="checkbox" name="remember" />
                    <p>Remember me</p>
                </div>
                <br/>
                <input class="primary-btn" type="submit" name="login" value="Login" />
            </form>
            <?php array_to_html(obj_to_array($user->get_errors())); ?>
            <a href="#">Forgot Password?</a>
            <a href="#">Can't access your account?</a>
            <h5>Not a member yet? <span class="bold"><a href="<?=ROOT?>/register">Register now</a></span>
            and get started!
            </h5>       
        </div>
    </div>
    <script type="text/javascript" src="<?=ROOT?>/assets/js/image.js"></script>
</body>
</html>