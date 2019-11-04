
document.getElementById('links').onclick = function(event) {
    event = event || window.event
    var target = event.target || event.srcElement,
      link = target.src ? target.parentNode : target,
      options = { index: link, event: event },
      links = this.getElementsByTagName('a')
    blueimp.Gallery(links, options)
};

document.getElementById('links2').onclick = function(event) {
    event = event || window.event
    var target = event.target || event.srcElement,
      link = target.src ? target.parentNode : target,
      options = { index: link, event: event },
      links = this.getElementsByTagName('a')
    blueimp.Gallery(links, options)
};

    Dropzone.options.dropzoneForm = {
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 2, // MB
        dictDefaultMessage: "<strong>Drop files here or click to upload. </strong></br> (This is just a demo dropzone. Selected files are not actually uploaded.)"
    };

$(document).ready(function(){

    var editor_one = CodeMirror.fromTextArea(document.getElementById("code1"), {
        lineNumbers: true,
        matchBrackets: true
    });

    var editor_two = CodeMirror.fromTextArea(document.getElementById("code2"), {
        lineNumbers: true,
        matchBrackets: true
    });

    var editor_two = CodeMirror.fromTextArea(document.getElementById("code3"), {
        lineNumbers: true,
        matchBrackets: true
    });

});
