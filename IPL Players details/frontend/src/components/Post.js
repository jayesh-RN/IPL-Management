import React, { useState } from "react";
import AddPayment from "./AddPayment";

const Post = (props) => {
  // const log = useContext(LogContext)

  const [player, setPlayer] = useState({
    player_name: "",
    team_id: "",
    role: "",
    age: "",
    matches_played: "",
  });
  const createPlayer = async () => {
    await AddPayment(player);
  };
  const validate = (e) => {
    e.preventDefault();
    if (
      e.target.player_name.value === "" ||
      e.target.team_id.value === "" ||
      e.target.role.value === "" ||
      e.target.age.value === "" ||
      e.target.matches_played.value === ""
    ) {
      alert("please fill all valid data");
      return false;
    } else {
      createPlayer();
    }
  };

  const onChange = (e) => {
    setPlayer({ ...player, [e.target.id]: e.target.value });
  };

  return (
    <>
      <h1 className="text-center mb-5" style={{ color: "#007bff" }}>
        Create an action method to inserts a new Player in the database.
      </h1>
      <form className="form-group container" onSubmit={validate}>
        <div className="form-group">
          <label htmlFor="player_name">Player Name : </label>
          <input
            type="text"
            className="form-control"
            id="player_name"
            value={player.player_name}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="team_id">team_id :</label>
          <input
            type="number"
            className="form-control"
            id="team_id"
            value={player.team_id}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="role">Role : </label>
          <input
            type="text"
            className="form-control"
            id="role"
            value={player.role}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="age">age</label>
          <input
            type="number"
            className="form-control"
            id="age"
            value={player.age}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="matches_played">matches_played</label>
          <input
            type="number"
            className="form-control"
            id="matches_played"
            value={player.matches_played}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <button
          type="submit"
          className="btn btn-primary m-2 p-2"
          style={{ borderRadius: "10px" }}
        >
          Add Player
        </button>
      </form>
    </>
  );
};

export default Post;
