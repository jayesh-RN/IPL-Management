import axios from "axios";

const AddPlayer = async (player) => {
  const URL = `http://localhost:5009/q1`;
  await axios
    .post(URL, player)
    .then(() => {
      alert("Player added successfully");
    })
    .catch((err) => {
      alert("Error 400: Bad Request. Please check your input data.");
    });
};

export default AddPlayer;
