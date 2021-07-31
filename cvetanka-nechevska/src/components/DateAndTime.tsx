import React, { useState } from "react";

import { Box, Button, TextField } from "@material-ui/core";
import { MuiPickersUtilsProvider, DatePicker } from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import "date-fns";

import { DEFAULT_TIME } from "../constants/constants";


const DateAndTime = (props: { handleChange: (arg: any) => void }) => {
  const [selectedDateAndTime, setSelectedDateAndTime] = useState({
    date: new Date(),
    time: DEFAULT_TIME,
  });

  const handleDateChange = (date: Date | null) => {
    setSelectedDateAndTime({
      ...selectedDateAndTime,
      date: new Date(date!),
    });
  };

  const onChange = (event: { target: { value: string } }) => {
    setSelectedDateAndTime({
      ...selectedDateAndTime,
      time: event.target.value,
    });
  };

  const addDate = () => {
    let month = (selectedDateAndTime.date.getMonth() + 1).toString();
    let day = selectedDateAndTime.date.getDate().toString();

    if (month.length < 2) month = "0" + month;
    if (day.length < 2) day = "0" + day;

    let formattedDateAndTime =
      selectedDateAndTime.date.getFullYear() +
      "-" +
      month +
      "-" +
      day +
      " " +
      selectedDateAndTime.time;

    props.handleChange(formattedDateAndTime);
  };

  return (
    <Box
      display="flex"
      width="120px"
      flexDirection="column"
      justifyContent="center"
      mt={3}
      border="1px solid"
      borderRadius="10px"
      p={1}
    >
      <Box padding="10px">
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <DatePicker
          autoOk
          label="Date"
          value={selectedDateAndTime.date}
          onChange={handleDateChange}
        />
      </MuiPickersUtilsProvider>
      </Box>
      <Box padding="10px">
      <TextField
        label="Time"
        type="time"
        fullWidth
        value={selectedDateAndTime.time}
        onChange={onChange}
        InputLabelProps={{
          shrink: true,
        }}
        inputProps={{
          step: 300, // 5 min
        }}
      />
      </Box>
      <Button variant="outlined" color="primary" onClick={addDate}>Add</Button>
    </Box>
  );
};

export default DateAndTime;
