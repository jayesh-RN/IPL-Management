import React, { useEffect, useState } from "react";
import { GetDataQ3 } from "./GetDataQ3";

const Q2 = () => {
  const [q3, setq3] = useState([]);

  useEffect(() => {
    const getDataQ3 = async () => {
      let data = await GetDataQ3();
      if (Array.isArray(data)) {
        setq3(data);
      } else {
        console.error("Invalid response from GetDataQ4");
      }
    };
    getDataQ3();
  }, []);

  return (
    <>
      <h1 className="text-center mb-5" style={{ color: "#007bff" }}>
        Retrieve the top 5 players based on the number of matches played who
        have participated in matches with the highest fan engagements.
      </h1>
      <div className="container">
        <div className="row justify-content-center">
          <table
            className="table table-striped table-bordered"
            style={{ width: "80%" }}
          >
            <thead style={{ backgroundColor: "#007bff", color: "#ffffff" }}>
              <tr>
                <th>Player Name</th>
                <th>Matches Played</th>
                <th>Total fan engagements</th>
              </tr>
            </thead>
            <tbody>
              {q3.map((p) => (
                <tr key={p.player_name}>
                  <td>{p.player_name}</td>
                  <td>{p.matches_played}</td>
                  <td>{p.total_fan_engagements}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export default Q2;
