const start = document.getElementById("start");
const stop = document.getElementById("stop");
const video = document.querySelector("video");
let recorder, stream;

async function startRecording() {
    stream = await navigator.mediaDevices.getDisplayMedia({
        video: { mediaSource: "screen" }
    });
    recorder = new MediaRecorder(stream);

    const chunks = [];
    recorder.ondataavailable = e => chunks.push(e.data);
    recorder.onstop = e => {
        const completeBlob = new Blob(chunks, { type: chunks[0].type });
        video.src = URL.createObjectURL(completeBlob);
        postDataToServer(completeBlob);
    };

    recorder.start();
}

start.addEventListener("click", () => {
    start.setAttribute("disabled", true);
    stop.removeAttribute("disabled");

    startRecording();
});

stop.addEventListener("click", () => {
    stop.setAttribute("disabled", true);
    start.removeAttribute("disabled");

    recorder.stop();
    stream.getVideoTracks()[0].stop();
});

function postDataToServer(completeBlob)
{
    var fileName = (Math.round(Math.random() * 99999999) + 99999999) + '.webm';
    //Saving
    var formdata = new FormData(); //FormData object
    //add form data
    formdata.append(fileName, completeBlob);
    $.ajax({
        type: 'POST',
        url: '/Home/PostRecordedAudioVideo',
        data: formdata,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (msg) {
            alert("Done, Video Uploaded.");
        }
    });
};