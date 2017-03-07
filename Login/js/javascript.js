window.onload = function () {

    checkInputFormat();

    var costumer = {
        name: "User",
        password: "TestTest"
    };


    function stopLogin(evt) {
        evt.preventDefault();
    }

    document.getElementById("login").addEventListener('click', checkData

    );

    function checkData(eve) {

        var username = document.getElementById("user").value;
        var password = document.getElementById("password").value;
        var setName = costumer.name;
        var setPassword = costumer.password;

        if (username.length < 4 || password.length < 4) {
            eve.preventDefault();
        }
        else if (username === setName && password === setPassword) {
            successfullLogin();

        } else {
            unsuccessfullLogin();
        }

    }

    function successfullLogin() {

        var messageContainer = document.getElementById("messageContainer");
        var successfullMessage = document.createElement("div");
        successfullMessage.id = "successfullMessage"
        var textnode = document.createTextNode("You have successfully logged in.");
        successfullMessage.appendChild(textnode);
        messageContainer.appendChild(successfullMessage);

    };

    function unsuccessfullLogin() {

        var messageContainer = document.getElementById("messageContainer");
        var unsuccessfullMessage = document.createElement("div");
        unsuccessfullMessage.id = "unsuccessfullMessage"
        var textnode = document.createTextNode("User Name or Password is incorrect");
        unsuccessfullMessage.appendChild(textnode);
        messageContainer.appendChild(unsuccessfullMessage);
        //messageContainer.innerHTML=unsuccessfullMessage;
    };

    function incorrectFormat() {

        var username = document.getElementById("user").value;
        var usernameInput = document.getElementById("user")
        var password = document.getElementById("password").value;
        var passwordInput = document.getElementById("password")


        if (username.length > 0 && username.length < 4) {
            usernameInput.classList.add("incorrectFormat");
        }

        if (password.length > 0 && password.length < 4) {
            passwordInput.classList.add("incorrectFormat");
        }
    }


    function correctFormat() {

        var username = document.getElementById("user").value;
        var usernameInput = document.getElementById("user")
        var password = document.getElementById("password").value;
        var passwordInput = document.getElementById("password")


        if (username.length === 0 || username.length > 4) {
            usernameInput.classList.remove("incorrectFormat");
        }

        if (password.length === 0 || password.length > 4) {
            passwordInput.classList.remove("incorrectFormat");
        }
    }

    function checkInputFormat() {
        setInterval(function () { incorrectFormat() }, 50);
        setInterval(function () { correctFormat() }, 50);
    }

}