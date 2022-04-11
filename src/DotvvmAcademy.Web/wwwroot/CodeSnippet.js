"use strict";
var DotvvmAcademy;
(function (DotvvmAcademy) {
    function initializeCodeSnippets() {
        let headerTemplate = document.getElementsByClassName("code__header-template")[0];
        if (!headerTemplate) {
            return;
        }
        let snippets = document.querySelectorAll(".step-info-box pre");
        for (var i = 0; i < snippets.length; i++) {
            let snippet = snippets[i];
            for (var j = 0; j < headerTemplate.children.length; j++) {
                var clone = headerTemplate.children[j].cloneNode(true);
                snippet.appendChild(clone);
            }
            var button = snippet.getElementsByClassName("code__copy-button")[0];
            var code = snippet.getElementsByTagName("code")[0];
            button.addEventListener("click", () => {
                var range = document.createRange();
                range.selectNodeContents(code);
                window.getSelection().removeAllRanges();
                window.getSelection().addRange(range);
                document.execCommand("copy");
                window.getSelection().removeRange(range);
            });
        }
    }
    DotvvmAcademy.initializeCodeSnippets = initializeCodeSnippets;
})(DotvvmAcademy || (DotvvmAcademy = {}));
DotvvmAcademy.initializeCodeSnippets();
