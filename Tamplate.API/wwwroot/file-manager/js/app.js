import Auth from './auth.js';
import Gallery from './gallery.js';
class App{
   
    constructor() {
        this.apiBaseUrl = window.fileManagerConfig?.apiBaseUrl ?? window.location.origin;
        this.init();
    }

    init(){
        console.log("Works");
        const uploadForm = $('#myDropzone');

        if (uploadForm.length) {
            uploadForm.attr('action', `${this.apiBaseUrl}/File`);
        }

        // console.log(Config.API_URL);
    }

    
}
var app = new App();
var auth = new Auth();
var gallery = new Gallery();