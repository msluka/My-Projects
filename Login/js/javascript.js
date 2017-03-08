window.onload = function () {

    checkInputFormat();

    var costumer = {

        name: "User",
        password: "Test"
        
    };

    document.getElementById("login").addEventListener('click', checkData);


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
        var textnode = document.createTextNode("You have successfully logged in");
        var back = document.createElement("BUTTON");
        back.id = "back";
        var buttontxt = document.createTextNode("Back");
        back.appendChild(buttontxt);
        successfullMessage.appendChild(textnode);
        successfullMessage.appendChild(back);
        messageContainer.appendChild(successfullMessage);

    };

    function unsuccessfullLogin() {

        var messageContainer = document.getElementById("messageContainer");
        var unsuccessfullMessage = document.createElement("div");
        unsuccessfullMessage.id = "unsuccessfullMessage";
        var textnode = document.createTextNode("User Name or Password is incorrect");
        var tryAgain = document.createElement("BUTTON");
        tryAgain.id = "tryAgain";
        var buttontxt = document.createTextNode("Try Again");
        tryAgain.appendChild(buttontxt);
        unsuccessfullMessage.appendChild(textnode);
        unsuccessfullMessage.appendChild(tryAgain);
        messageContainer.appendChild(unsuccessfullMessage);

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

        if (username.length === 0 || username.length === 4 || username.length > 4) {
            usernameInput.classList.remove("incorrectFormat");
        }

        if (password.length === 0 || password.length === 4 || username.length > 4) {
            passwordInput.classList.remove("incorrectFormat");
        }
    }


    function checkInputFormat() {

        setInterval(function () { incorrectFormat() }, 50);
        setInterval(function () { correctFormat() }, 50);
    }



    document.addEventListener("click", function (event) {

        if (event.target.id === 'tryAgain') {

            var wrongMessage = document.getElementById("unsuccessfullMessage");
            document.getElementById("messageContainer").removeChild(wrongMessage);
        }

        if (event.target.id === 'back') {

            var successMessage = document.getElementById("successfullMessage");
            document.getElementById("messageContainer").removeChild(successMessage);
            document.getElementById("user").value = '';
            document.getElementById("password").value = '';
        }

    });

}
