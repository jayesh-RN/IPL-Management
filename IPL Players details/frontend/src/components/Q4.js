import React, { useEffect, useState } from "react";
import { GetDataQ4 } from "./GetDatQ4";
const Q4 = () => {
  //   const [q4, setq4] = useState([]);
  //   const [year, setYear] = useState("");
  //   const [input, setInput] = useState(""); // Add a state to hold the input value

  //   useEffect(() => {
  //     const getDataQ3 = async () => {
  //       let data = await GetDataQ4(year);
  //       if (data != null) {
  //         setq4(data);
  //       }
  //     };
  //     getDataQ3();
  //   }, []);

  //   const onChange = (e) => {
  //     setInput(e.target.value); // Update the state here
  //   };

  //   const onSubmit = (e) => {
  //     e.preventDefault();
  //     setYear(input);
  //   };

  const [q4, setq4] = useState([]);
  const [start, setStart] = useState("");
  const [end, setEnd] = useState("");
  const [input1, setInput1] = useState("");
  const [input2, setInput2] = useState("");

  useEffect(() => {
    const getDataQ4 = async () => {
      let data = await GetDataQ4(start,end);
      if (Array.isArray(data)) {
        setq4(data);
      } else {
        console.error("Invalid response from GetDataQ4");
      }
    };
    getDataQ4();
  }, [start, end]); // Changed input to year

  const onChange1 = (e) => {
    setInput1(e.target.value);
  };

  const onChange2 = (e) => {
    setInput2(e.target.value);
  };

  const onSubmit = (e) => {
    e.preventDefault();
    setStart(input1);
    setEnd(input2);
  };

  return (
    <div className="container mt-5">
      <h1 className="text-center">
        Retrieve all matches that were played within a specific date range. This
        endpoint retrieves a list of matches that took place between two
        specified dates, including match details such as date, venue, and teams
        involved.
      </h1>
      <form className="form-group" onSubmit={onSubmit}>
        <div className="form-group">
          <label htmlFor="start">Start date</label>
          <input
            type="date"
            className="form-control"
            id="start"
            value={input1}
            onChange={onChange1}
          />
        </div>
        <div className="form-group">
          <label htmlFor="end">End date</label>
          <input
            type="date"
            className="form-control"
            id="end"
            value={input2}
            onChange={onChange2}
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Get detail
        </button>
      </form>
      <h3 className="text-center mt-3">
        Matches that were played within a specific date range {q4.length}.
      </h3>
      <table className="table table-striped table-bordered mt-3">
        <thead>
          <tr>
            <th>Match Id</th>
            <th>Match date</th>
            <th>Venue </th>
            <th>Team 1</th>
            <th>Team 2</th>
          </tr>
        </thead>
        <tbody>
          {q4.map((p) => (
            <tr key={p.match_id}>
              <td>{p.match_id}</td>
              <td>{p.match_date}</td>
              <td>{p.venue}</td>
              <td>{p.team1_name}</td>
              <td>{p.team2_name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Q4;
