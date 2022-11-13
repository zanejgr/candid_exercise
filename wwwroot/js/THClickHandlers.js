function toggleIco(th) {
    console.log(th);
   const img = th.getElementsByTagName("img")[0];
   console.log(img.src.split('/').pop());
   img.src = img.src.split('/').pop() === "up.ico" ? "down.ico" : "up.ico";
}

function onClickId() {
    SortBy("Id");
    toggleIco(event.target);
}

function onClickFirstName() {
    SortBy("FirstName");
    toggleIco(event.target);
}

function onClickLastName() {
    SortBy("LastName");
    toggleIco(event.target);
}
