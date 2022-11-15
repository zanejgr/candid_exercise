function load(){
  const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });
  const CustomerId = document.getElementById("CustomerId");
  CustomerId.value=params.id; 
}

function submit() {
  const form = document.getElementById("form");
  if (form.checkValidity()) {
    const AddressTypeId = document.getElementById("AddressTypeId");
    const CustomerId = document.getElementById("CustomerId");
    const Country = document.getElementById("Country");
    const AddressLine1 = document.getElementById("AddressLine1");
    const AddressLine2 = document.getElementById("AddressLine2");
    const City = document.getElementById("City");
    const State = document.getElementById("State");
    const Zip = document.getElementById("Zip");

    fetch("/address", {
      method: "post",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        AddressTypeId: AddressTypeId.value,
        CustomerId: CustomerId.value,
        Country: Country.value,
        AddressLine1: AddressLine1.value,
        AddressLine2: AddressLine2.value,
        City: City.value,
        State: State.value,
        Zip: Zip.value,
      }),
    }).then((Response) => (window.location.href = "/index"));
  } else {
    form.reportValidity();
  }
}