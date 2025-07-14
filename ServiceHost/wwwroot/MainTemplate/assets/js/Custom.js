
const notyf = new Notyf({
    duration: 5000,
    position: { x: 'left', y: 'top' },
    dismissible: true,
    types: [
        {
            type: 'info',
            background: '#17a2b8',
            icon: {
                className: 'bi bi-info-circle-fill',
                tagName: 'i'
            }
        },
        {
            type: 'warning',
            background: '#ffc107',
            icon: {
                className: 'bi bi-exclamation-triangle-fill',
                tagName: 'i'
            }
        }
    ]
});

function ShowMessage(title, message, type) {
    const htmlMessage = `<strong>${title}</strong><br>${message}`;

    switch (type) {
        case 'success':
            notyf.success(htmlMessage);
            break;
        case 'error':
            notyf.error(htmlMessage);
            break;
        case 'warning':
            notyf.open({
                type: 'warning',
                message: htmlMessage
            });
            break;
        case 'info':
            notyf.open({
                type: 'info',
                message: htmlMessage
            });
            break;
        default:
            notyf.open({
                type: type || 'info',
                message: htmlMessage
            });
            break;
    }
}
