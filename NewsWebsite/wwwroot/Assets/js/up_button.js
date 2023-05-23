// Scroll Button Function

let mybutton = document.querySelector(".up__button");

window.onscroll = function () {
  scrollFunction();
};

function scrollFunction() {
  if (
    document.body.scrollTop > 20 ||
    document.documentElement.scrollTop > 20
  ) {
    mybutton.style.display = "block";
  } else {
    mybutton.style.display = "none";
  }
}

const box = document.querySelectorAll('.reveal');

window.addEventListener('scroll', checkBoxes);

checkBoxes();

function checkBoxes() {
  const triggerBottom = window.innerHeight / 5 * 3.75;
  
  box.forEach(box => {
    const boxTop = box.getBoundingClientRect().top;
    
    if(boxTop < triggerBottom) {
      box.classList.add('active');
    } else {
      box.classList.remove('active');
    }
  })
}
