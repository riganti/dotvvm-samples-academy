let IsLessonPageOpen = document.querySelector("section").classList.contains("section-lesson");

if (IsLessonPageOpen) {
    generateCodeHeader();
    generateCopyButton();

    let allCopyButtons = document.querySelectorAll(".code__copy-button");

    for (let CopyButton of Array.from(allCopyButtons)) {
        CopyButton.addEventListener("click", function () {
            let parent = this.parentNode;
            let respondingCode = parent.previousElementSibling;
            respondingCode.classList.add('red');
            var range = document.createRange();
            range.selectNodeContents(respondingCode);
            window.getSelection().removeAllRanges();
            window.getSelection().addRange(range);
            document.execCommand("copy")
        });
    }
}


function generateCodeHeader() {
    var pre = document.querySelectorAll('pre');
    for (var i = 0; i < pre.length; i++) {
        var newDiv = document.createElement("div");
        var newButton = document.createElement("button");
        newDiv.classList.add("code__header");
        pre[i].appendChild(newDiv);
    }
}

function generateCopyButton() {
    var codeHeader = document.querySelectorAll('.code__header');
    for (var i = 0; i < codeHeader.length; i++) {
        var newButton = document.createElement("button");
        newButton.classList.add("code__copy-button");
        newButton.setAttribute('title', 'Copy to Clipboard');
        codeHeader[i].appendChild(newButton);
    }
}