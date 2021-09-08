function scrollBottom(elementId, height) {
    document.getElementById(elementId).scrollBy({
        top: height * Math.floor((document.getElementById(elementId).offsetHeight / height)), left: 0, behavior: "smooth"
    })
}