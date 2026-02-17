export default class Auth{
    constructor() {
        this.tokenKey = 'jwtToken';
        this.init();
    }

    init(){
        this.setUp();
        this.login();
    }

    setUp(){
        this.authBtn = $("#auth");

        if (!localStorage.getItem(this.tokenKey)) {
            localStorage.setItem(this.tokenKey, '');
        }

        
    }
    handleLogin(){
        console.log('Radi');
        this.username = $("[aria-label='Username']").val();
        this.password = $("[aria-label='Password']").val();
        console.log(this.username,this.password);

        if(this.username === '' || this.password === ''){
            alert("Missing password or username");
        }else{
            $.ajax({
                type: "POST",
                url: `/Auth/login`,
                data: JSON.stringify({
                    username:this.username,
                    password:this.password
                }),
                contentType: "application/json",
                success: function (response) {
                    console.log(response)
                }
            });
        }

    }
    login(){
        this.authBtn.off('click');
        this.authBtn.on('click', this.handleLogin);

    }
}