export default class Auth{
    constructor() {
        this.tokenKey = 'jwtToken';
        this.modalId = '#authModal';
        this.apiBaseUrl = window.fileManagerConfig?.apiBaseUrl ?? window.location.origin;
        this.init();
    }

    init(){
        this.setUp();
        this.refreshAuthState();
        this.login();
        this.logout();
    }

    setUp(){
        this.authBtn = $("#auth");
        this.logoutBtn = $("#clearAuth");
        this.authStatus = $("#authStatus");
        this.usernameInput = $("#authUsername");
        this.passwordInput = $("#authPassword");

        if (!localStorage.getItem(this.tokenKey)) {
            localStorage.setItem(this.tokenKey, '');
        }
        
    }

    handleLogin(){
        const username = this.usernameInput.val();
        const password = this.passwordInput.val();

        if(username === '' || password === ''){
            alert("Missing password or username");
        }else{
            $.ajax({
                type: "POST",
                url: `${this.apiBaseUrl}/Auth/login`,
                data: JSON.stringify({
                    username: username,
                    password: password
                }),
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    localStorage.setItem('jwtToken', response.token ?? '');
                    this.refreshAuthState();
                    $(document).trigger('auth:changed', [{ authenticated: true }]);
                    const modalElement = $(this.modalId)[0];
                    const modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
                    modalInstance.hide();
                }.bind(this),
                error: function () {
                    alert('Login failed');
                }
            });
        }

    }
    
    login(){
        this.authBtn.off('click').on('click', this.handleLogin.bind(this));
    }

    logout(){
        this.logoutBtn.off('click').on('click', function () {
            localStorage.removeItem(this.tokenKey);
            localStorage.setItem(this.tokenKey, '');
            this.refreshAuthState();
            $(document).trigger('auth:changed', [{ authenticated: false }]);
            this.usernameInput.val('');
            this.passwordInput.val('');
        }.bind(this));
    }

    refreshAuthState(){
        const token = localStorage.getItem(this.tokenKey);

        if (token) {
            this.authStatus.text('Authorized').removeClass('text-bg-secondary').addClass('text-bg-success');
        } else {
            this.authStatus.text('Not authorized').removeClass('text-bg-success').addClass('text-bg-secondary');
        }

    }
}