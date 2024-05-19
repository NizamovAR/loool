//const photoUploader = document.getElementById('photo-uploader');
//const photoUploader1 = document.getElementById('photo-uploader1');
//const photoUploader2 = document.getElementById('photo-uploader2');
//const photoUploader3 = document.getElementById('photo-uploader3');



//photoUploader.addEventListener('change', (event) => {
//    const file = photoUploader.files[0];
//    var fileExtension = ['jpg', 'jpeg', 'png'];
//    var ext = file.name.split('.').pop().toLowerCase();
//    var isPhoto = false;
//    fileExtension.forEach(function (elem) {
//        if (elem == ext) {
//            isPhoto = true;
//        }
//    }
//    )
//    if (isPhoto === false) {
//        photoUploader.value = "";
//        const image = document.getElementById('uploadingMealImage');
//        image.src = "";
//    }
//    else {
//        const image = document.getElementById('uploadingMealImage');
//        var fileReader = new FileReader();
//        fileReader.onload = function () {
//            image.src = fileReader.result;
//        }
//        fileReader.readAsDataURL(photoUploader.files[0]);
//    }
//});

//photoUploader1.addEventListener('change', (event) => {
//    const file = photoUploader1.files[0];
//    var fileExtension = ['jpg', 'jpeg', 'png'];
//    var ext = file.name.split('.').pop().toLowerCase();
//    var isPhoto = false;
//    fileExtension.forEach(function (elem) {
//        if (elem == ext) {
//            isPhoto = true;
//        }
//    }
//    )
//    if (isPhoto === false) {
//        photoUploader1.value = "";
//        const image = document.getElementById('uploadingMealImage1');
//        image.src = "";
//    }
//    else {
//        const image = document.getElementById('uploadingMealImage1');
//        var fileReader = new FileReader();
//        fileReader.onload = function () {
//            image.src = fileReader.result;
//        }
//        fileReader.readAsDataURL(photoUploader1.files[0]);
//    }
//});

//photoUploader2.addEventListener('change', (event) => {
//    const file = photoUploader2.files[0];
//    var fileExtension = ['jpg', 'jpeg', 'png'];
//    var ext = file.name.split('.').pop().toLowerCase();
//    var isPhoto = false;
//    fileExtension.forEach(function (elem) {
//        if (elem == ext) {
//            isPhoto = true;
//        }
//    }
//    )
//    if (isPhoto === false) {
//        photoUploader2.value = "";
//        const image = document.getElementById('uploadingMealImage2');
//        image.src = "";
//    }
//    else {
//        const image = document.getElementById('uploadingMealImage2');
//        var fileReader = new FileReader();
//        fileReader.onload = function () {
//            image.src = fileReader.result;
//        }
//        fileReader.readAsDataURL(photoUploader2.files[0]);
//    }
//});
const fileInput = document.getElementById('photo-uploader');
const fileInput1 = document.getElementById('photo-uploader1');
const fileInput2 = document.getElementById('photo-uploader2');
const fileInput3 = document.getElementById('photo-uploader3');

const image = document.getElementById('uploadingMealImage');
const image1 = document.getElementById('uploadingMealImage1');
const image2 = document.getElementById('uploadingMealImagе2');
const image3 = document.getElementById('uploadingMealImagе3');

document.addEventListener('DOMContentLoaded', function () {
    fileInput.addEventListener('change', function () {
        const file = fileInput.files[0];
        const reader = new FileReader();

        reader.onload = function (e) {
            image.src = e.target.result;
        }

        reader.readAsDataURL(file);
    });

    fileInput1.addEventListener('change', function () {
        const file = fileInput1.files[0];
        const reader = new FileReader();

        reader.onload = function (e) {
            image1.src = e.target.result;
            console.log(image2);
        }

        reader.readAsDataURL(file);
    });

    fileInput2.addEventListener('change', function () {
        const file = fileInput2.files[0];
        const reader = new FileReader();

        reader.onload = function (e) {
            image2.src = e.target.result;
        }

        reader.readAsDataURL(file);
    });
    fileInput3.addEventListener('change', function () {
        const file = fileInput3.files[0];
        const reader = new FileReader();

        reader.onload = function (e) {
            image3.src = e.target.result;
        }

        reader.readAsDataURL(file);
    });
});
//function sendImages() {
//    const images = document.querySelectorAll('img[name^="photo"]');
//    const imageSources = [];
//    const stepTexts = [];
//    document.querySelectorAll('textarea[name^="stepText"]').forEach(textarea => {
//        stepTexts.push(textarea.value);
//        images.forEach(image => {
//            imageSources.push(image.src);
//        });
//        fetch('/home/addingMeal', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify({ images: imageSources, steps: stepTexts })
//        })
//            .then(response => response.json())
//            .then(data => {
//                console.log(data);
//            })
//            .catch(error => {
//                console.error('Error:', error);
//            });
//    });
//});
