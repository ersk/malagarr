

function alertUser() {

    console.log("## alertUser ################################################1");
    alert('The button was selected! 2');
}

function addHandlers() {

    console.log("## addHandlers ################################################2");
    const btn = document.getElementById("btn");
    //btn.addEventListener("click", alertUser);

    //console.log("## addHandlers ################################################1");
    alert('addHandlers');
}

async function openDirectoryAsync() {

    console.log("## openDirectoryAsync ################################################");
    // FileSystemDirectoryHandle
    const dirHandle = await window.showDirectoryPicker();

    return dirHandle;
}
async function getContents(dirHandle) {

    console.log("## getContents ################################################");

    //let entries = {};
    let keysArr = [];
    for await (const [key, value] of dirHandle.entries()) {

        console.log("key = " + key);
        //entries[key] = value;
        keysArr.push(key);
    }
    return keysArr;
}
async function getDirectoryHandle(dirHandle, childKey) {

    console.log("## getDirectoryHandle ################################################");

    let childHandle = await dirHandle.getDirectoryHandle(childKey);
    return childHandle;
}
async function getFileHandle(dirHandle, childKey) {

    console.log("## getFileHandle ################################################");

    let fileHandle = await dirHandle.getFileHandle(childKey);

    return fileHandle;
}

async function getFileText(fileHandle) {

    console.log("## getFileText ################################################");

    let file = await fileHandle.getFile();

    let text = await file.text();

    return text;
}

//async function getFileStream(fileHandle) {

//    console.log("## getFileStream ################################################");

//    let file = await fileHandle.getFile();
//    return file;
//    //let text = await file.text();
//    //console.log(text);

//    //let stream = file.stream();

//    //return stream;
//}



export { alertUser, addHandlers, openDirectoryAsync, getContents, getDirectoryHandle, getFileHandle, getFileText };