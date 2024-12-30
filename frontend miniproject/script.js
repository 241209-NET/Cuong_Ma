
document.getElementById('alert-button').addEventListener('click', function () {
    alert('Hello, this is an alert message!');
});

const paragraph = document.getElementById('about-paragraph');
paragraph.addEventListener('mouseover', function () {
    paragraph.style.color = 'blue';
});
paragraph.addEventListener('mouseout', function () {
    paragraph.style.color = 'black';
});