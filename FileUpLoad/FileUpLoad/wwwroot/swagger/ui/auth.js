var auth = window.auth || {};
auth.tokenCookieName = "aspnetcore.authauth";
auth.loginUrl = "/api/Auth/Login";
auth.logoutUrl = "/api/Auth/Logout";
//登出
auth.logout = function (callback)
{
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function ()
    {
        if (xhr.readyState === XMLHttpRequest.DONE)
        {
            if (xhr.status === 200)
            {
                var res = JSON.parse(xhr.responseText);
                if (!res.success)
                {
                    console.warn(res.data);
                    return;
                }
                localStorage.removeItem("auth.hasLogin");
                callback();
            }
            else
            {
                console.warn("Logout failed !");
            }
        }
    };
    xhr.open('Get', auth.logoutUrl, true);
    xhr.send();
}

auth.hasLogin = function ()
{
    return localStorage.getItem("auth.hasLogin");
}

//打开登陆对话框布局
auth.openAuthDialog = function (loginCallback)
{
    auth.closeAuthDialog();
    /**************创建弹窗框********************/
    var authAuthDialog = document.createElement('div');
    authAuthDialog.className = 'dialog-ux';
    authAuthDialog.id = 'auth-auth-dialog';
    document.getElementsByClassName("swagger-ui")[1].appendChild(authAuthDialog);

    // -- backdrop-ux
    var backdropUx = document.createElement('div');
    backdropUx.className = 'backdrop-ux';
    authAuthDialog.appendChild(backdropUx);

    // -- modal-ux
    var modalUx = document.createElement('div');
    modalUx.className = 'modal-ux';
    authAuthDialog.appendChild(modalUx);

    // -- -- modal-dialog-ux
    var modalDialogUx = document.createElement('div');
    modalDialogUx.className = 'modal-dialog-ux';
    modalUx.appendChild(modalDialogUx);

    // -- -- -- modal-ux-inner
    var modalUxInner = document.createElement('div');
    modalUxInner.className = 'modal-ux-inner';
    modalDialogUx.appendChild(modalUxInner);

    // -- -- -- -- modal-ux-header
    var modalUxHeader = document.createElement('div');
    modalUxHeader.className = 'modal-ux-header';
    modalUxInner.appendChild(modalUxHeader);
    var modalHeader = document.createElement('h3');
    modalHeader.innerText = 'Authorize';
    modalUxHeader.appendChild(modalHeader);

    // -- -- -- -- modal-ux-content
    var modalUxContent = document.createElement('div');
    modalUxContent.className = 'modal-ux-content';
    modalUxInner.appendChild(modalUxContent);
    modalUxContent.onkeydown = function (e)
    {
        if (e.keyCode === 13)
        {
            //try to login when user presses enter on authorize modal
            auth.login(loginCallback);
        }
    };

    //Inputs
    createInput(modalUxContent, 'userName', 'Username or email address');
    createInput(modalUxContent, 'password', 'Password', 'password');

    /***************底下两个按钮Buttons，一个关闭，一个登陆*****************/
    var authBtnWrapper = document.createElement('div');
    authBtnWrapper.className = 'auth-btn-wrapper';
    modalUxContent.appendChild(authBtnWrapper);

    //Close button
    var closeButton = document.createElement('button');
    closeButton.className = 'btn modal-btn auth btn-done button';
    closeButton.innerText = 'Close';
    closeButton.style.marginRight = '5px';
    closeButton.onclick = auth.closeAuthDialog;
    authBtnWrapper.appendChild(closeButton);

    //Authorize button
    var authorizeButton = document.createElement('button');
    authorizeButton.className = 'btn modal-btn auth authorize button';
    authorizeButton.innerText = 'Login';
    authorizeButton.onclick = function ()
    {
        auth.login(loginCallback);
    };
    authBtnWrapper.appendChild(authorizeButton);
    /**********************************************************************/
    /**********************************************************************/
}

//关闭登陆对话框
auth.closeAuthDialog = function ()
{
    if (document.getElementById('auth-auth-dialog'))
    {
        document.getElementsByClassName("swagger-ui")[1].removeChild(document.getElementById('auth-auth-dialog'));
    }
}

//登陆操作
auth.login = function (callback)
{
    var usernameOrEmailAddress = document.getElementById('userName').value;
    if (!usernameOrEmailAddress)
    {
        alert('Username or Email Address is required, please try with a valid value !');
        return false;
    }

    var password = document.getElementById('password').value;
    if (!password)
    {
        alert('Password is required, please try with a valid value !');
        return false;
    }
    //开始验证
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function ()
    {
        if (xhr.readyState === XMLHttpRequest.DONE)
        {
            if (xhr.status === 200)
            {
                var res = JSON.parse(xhr.responseText);
                if (!res.success)
                {
                    alert(res.data);
                    return;
                }
                localStorage.setItem("auth.hasLogin", true);
                callback();
            }
            else
            {
                alert('Login failed !');
            }
        }
    };
    //xhr.open('POST', auth.loginUrl, true);
    xhr.open('POST', auth.loginUrl, true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.send("username=" + encodeURIComponent(usernameOrEmailAddress) + "&password=" + password);
}

function createInput(container, id, title, type)
{
    var wrapper = document.createElement('div');
    wrapper.className = 'wrapper';
    container.appendChild(wrapper);
    var label = document.createElement('label');
    label.innerText = title;
    wrapper.appendChild(label);
    var section = document.createElement('section');
    section.className = 'block-tablet col-10-tablet block-desktop col-10-desktop';
    wrapper.appendChild(section);
    var input = document.createElement('input');
    input.id = id;
    input.type = type ? type : 'text';
    input.style.width = '100%';
    section.appendChild(input);
}