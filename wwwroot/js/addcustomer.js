function submit() {
  const form = document.getElementById("form");
  if (form.checkValidity()) {
    const fname = document.getElementById("fname");
    const lname = document.getElementById("lname");
    fname.checkValidity();
    lname.checkValidity();
    fetch("/customer", {
      method: "post",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        FirstName: fname.value,
        LastName: lname.value,
      }),
    }).then((Response) => (window.location.href = "/index"));
  } else {
    form.reportValidity();
  }
}
