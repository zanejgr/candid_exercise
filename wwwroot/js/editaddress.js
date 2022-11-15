function load() {
  const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });
  const AddressId = document.getElementById("AddressId");
  AddressId.value = params.id;

  const AddressTypeId = document.getElementById("AddressTypeId");
  const Country = document.getElementById("Country");
  const AddressLine1 = document.getElementById("AddressLine1");
  const AddressLine2 = document.getElementById("AddressLine2");
  const City = document.getElementById("City");
  const State = document.getElementById("State");
  const Zip = document.getElementById("Zip");

  AddressTypeId.value = params.AddressTypeId;
  Country.value = params.Country;
  AddressLine1.value = params.AddressLine1;
  AddressLine2.value = params.AddressLine2;
  City.value = params.City;
  State.value = params.State;
  Zip.value = params.Zip;
}
function submit() {
  const form = document.getElementById("form");
  if (form.checkValidity()) {
    const AddressTypeId = document.getElementById("AddressTypeId");
    const AddressId = document.getElementById("AddressId");
    const Country = document.getElementById("Country");
    const AddressLine1 = document.getElementById("AddressLine1");
    const AddressLine2 = document.getElementById("AddressLine2");
    const City = document.getElementById("City");
    const State = document.getElementById("State");
    const Zip = document.getElementById("Zip");

    fetch(`/address/${AddressId.value}`, {
      method: "put",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        AddressTypeId: AddressTypeId.value,
        Country: Country.value,
        AddressLine1: AddressLine1.value,
        AddressLine2: AddressLine2.value,
        City: City.value,
        State: State.value,
        Zip: Zip.value,
      }),
    }).then((Response) => window.location.href='/index.html');
  } else {
    form.reportValidity();
  }
}
