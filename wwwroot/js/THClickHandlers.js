function toggleIco(th) {
  console.log(th);
  const img = th.getElementsByTagName("img")[0];
  console.log(img.src.split("/").pop());
  img.src = img.src.split("/").pop() === "up.ico" ? "down.ico" : "up.ico";
}

function setIcoDown(th) {
  console.log(th);
  const img = th.getElementsByTagName("img")[0];
  img.src = "down.ico";
}

function onClickId() {
  SortBy("Id");
  syncIco(event.currentTarget.parentElement);
}

function onClickFirstName() {
  SortBy("FirstName");
  syncIco(event.currentTarget.parentElement);
}

function onClickLastName() {
  SortBy("LastName");
  syncIco(event.currentTarget.parentElement);
}

function syncIco(thead) {
  console.log(thead);
  const current = {
    col: localStorage.getItem("col"),
    desc: localStorage.getItem("desc") ?? "false",
  };
  [...thead.children].forEach((element) => {
    const img = element.getElementsByTagName("img")[0];
    img.src = "up.ico";
  });
  if ((current.desc === "true")) {
    switch (current.col) {
      case "id":
        setIcoDown(thead.children[0]);
        break;
      case "firstname":
        setIcoDown(thead.children[1]);
        break;

      case "lastname":
        setIcoDown(thead.children[2]);
        break;
      default:
        break;
    }
  }
}
