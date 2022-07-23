function activateNote(n)
{
    var note = document.getElementById("key-" + n);

    note.classList.add("note-on");
}

function deactivateNote(n)
{
    var note = document.getElementById("key-" + n);

    note.classList.remove("note-on");
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}