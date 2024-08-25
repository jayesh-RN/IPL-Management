import axios from "axios";

const AddPayment = async (payment) => {
  const URL = `http://localhost:5297/q1`;
    await axios
      .post(URL, payment)
      .then(() => {
        alert("Payment created successfully");
      })
      .catch((err) => {
        alert("Error 400: Bad Request. Please check your input data.");
      });
};

export default AddPayment;
