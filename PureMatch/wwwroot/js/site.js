// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction() {
    var passwordField = document.getElementById("inputField");
    var passicon = document.getElementById("eyeIcon"); 

    if (passwordField.type === "password") {
        passwordField.type = "text";
        document.getElementById("eyeIcon").src = "/images/eye_hide.png"
        document.getElementById("passmessage").innerText = "Skjul adgangskode";
        
    }
    else {
        passwordField.type = "password";
        document.getElementById("eyeIcon").src = "/images/eye.png"
        document.getElementById("passmessage").innerText = "Vis adgangskode";


    }

  
}

function mySubmit() {
    var pwdObj = document.getElementById('inputField');
    var hashObj = new jsSHA("SHA-512", "TEXT", { numRounds: 5 });
    hashObj.update(pwdObj.value);
    var hash = hashObj.getHash("HEX");
    pwdObj.value = hash;
    console.log(pwdObj.value.substring(0, 50));
}

