
function SendMessage() {
    window.createNotification({
        closeOnclick = true,
        displayCloseButton = false,
        positionClass = "nfc-top-right",
        showDuration = 5000,
        theme = theme != '' ? theme : 'success'
    })({
        title = title != '' ? title : 'اعلان',
        message = decodeURI(text)
    })
}