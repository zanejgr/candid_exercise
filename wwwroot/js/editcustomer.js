var id;
function load() {
  const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });
  const fname = document.getElementById("fname");
  const lname = document.getElementById("lname");

  fname.value=params.fname;
  lname.value=params.lname;
  id=params.id;
}

function submit() {
  const form = document.getElementById("form");
  if (form.checkValidity()) {
    const fname = document.getElementById("fname");
    const lname = document.getElementById("lname");
    fname.checkValidity();
    lname.checkValidity();
    fetch(`/customer/${id}`, {
      method: "put",
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
