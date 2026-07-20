export default class Gallery {
    constructor() {
        this.apiBaseUrl = window.fileManagerConfig?.apiBaseUrl ?? window.location.origin;
        this.tokenKey = 'jwtToken';
        this.filesGrid = document.querySelector('#filesGrid');
        this.loadingState = document.querySelector('#loadingState');
        this.emptyState = document.querySelector('#emptyState');
        this.totalFiles = document.querySelector('#totalFiles');
        this.totalImages = document.querySelector('#totalImages');
        this.totalSize = document.querySelector('#totalSize');

        document.addEventListener('auth:changed', (event) => {
            if (event.detail?.authenticated) {
                this.loadImages();
            } else {
                this.clear();
            }
        });

        if (localStorage.getItem(this.tokenKey)) {
            this.loadImages();
        }
    }

    async loadImages() {
        const token = localStorage.getItem(this.tokenKey);

        if (!token) {
            this.clear();
            return;
        }

        this.setLoading(true);

        $.ajax({
            type: 'GET',
            url: `${this.apiBaseUrl}/File/list?subfolder=Images`,
            headers: {
                Authorization: `Bearer ${token}`
            },
            dataType: 'json',
            success: function (response) {
                const files = response.files ?? [];
                this.render(files);
            }.bind(this),
            error: function (error) {
                console.error(error);
                this.clear();
            }.bind(this),
            complete: function () {
                this.setLoading(false);
            }.bind(this)
        });
    }

    render(files) {
        if (!this.filesGrid) {
            return;
        }

        const imageFiles = files.filter((file) => this.isImage(file.extension));
        const totalSize = imageFiles.reduce((sum, file) => sum + (file.size ?? 0), 0);

        this.totalFiles.textContent = String(imageFiles.length);
        this.totalImages.textContent = String(imageFiles.length);
        this.totalSize.textContent = this.formatBytes(totalSize);

        if (imageFiles.length === 0) {
            this.filesGrid.innerHTML = '';
            this.emptyState.style.display = 'block';
            return;
        }

        this.emptyState.style.display = 'none';
        this.filesGrid.innerHTML = imageFiles.map((file) => this.createCard(file)).join('');
    }

    createCard(file) {
        const name = file.name ?? 'Untitled';
        const url = file.url ?? '#';

        return `
            <div class="col-md-4 col-lg-3">
                <div class="card file-card h-100">
                    <img src="${url}" class="card-img-top file-preview" alt="${name}">
                    <div class="card-body">
                        <h6 class="card-title text-truncate" title="${name}">${name}</h6>
                        <p class="card-text small text-muted mb-0">${this.formatBytes(file.size ?? 0)}</p>
                    </div>
                </div>
            </div>
        `;
    }

    setLoading(isLoading) {
        if (this.loadingState) {
            this.loadingState.style.display = isLoading ? 'block' : 'none';
        }
    }

    clear() {
        if (this.filesGrid) {
            this.filesGrid.innerHTML = '';
        }

        if (this.totalFiles) {
            this.totalFiles.textContent = '0';
        }

        if (this.totalImages) {
            this.totalImages.textContent = '0';
        }

        if (this.totalSize) {
            this.totalSize.textContent = '0 Bytes';
        }

        if (this.emptyState) {
            this.emptyState.style.display = 'block';
        }
    }

    isImage(extension) {
        const value = (extension ?? '').toLowerCase();
        return ['.jpg', '.jpeg', '.png', '.gif', '.webp'].includes(value);
    }

    formatBytes(bytes) {
        if (!bytes) {
            return '0 Bytes';
        }

        const units = ['Bytes', 'KB', 'MB', 'GB'];
        const index = Math.floor(Math.log(bytes) / Math.log(1024));
        const value = bytes / Math.pow(1024, index);
        return `${value.toFixed(value >= 10 || index === 0 ? 0 : 1)} ${units[index]}`;
    }
}
