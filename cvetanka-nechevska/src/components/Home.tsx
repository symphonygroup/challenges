import React, { useState } from "react";
import { Box, styled } from "@material-ui/core";

import DateAndTime from "./DateAndTime";
import SelectVehicleType from "./SelectVehicleType";
import ListingFormattedDates from "./ListingFormattedDates";

import { getTax } from "../utils/utils";

import { DEFAULT_VECHICLE, DEFINED_CLASSES } from "../constants/constants";

const Container = styled(Box)({
  display: "flex",
  width: "100%",
  flexDirection: "column",
  justifyContent: "center",
  alignItems: "center",
  marginTop: "100px",
});

const Home = () => {
  const [selectedVehicle, setVehicle] = useState(DEFAULT_VECHICLE);
  const [selectedDates, setDates] = useState<Date[]>([]);

  const handleVehicleChange = (event: React.ChangeEvent<{ value: string }>) => {
    setVehicle(event.target.value);
  };

  const handleDateChange = (input: Date) => {
    if (!selectedDates.includes(input)) {
      setDates([...selectedDates, input]);
    }
  };

  const handleDeleteDate = (newDate: Date) => {
    setDates(selectedDates.filter((date) => date !== newDate));
  };

  const dynamicClass = (name: string) => {
    return DEFINED_CLASSES[name];
  };

  const classInstance = dynamicClass(selectedVehicle);

  const caluclateFollFee = () => {
    return getTax(new classInstance(), selectedDates);
  };

  return (
    <Container>
      <>Congestion Tax Calculator - Gothenburg</>
      <SelectVehicleType
        vechileType={selectedVehicle}
        handleChange={handleVehicleChange}
      />
      <DateAndTime handleChange={handleDateChange} />
      <ListingFormattedDates
        dates={selectedDates}
        handleDeleteDate={handleDeleteDate}
      />
      <>Total toll fee: {caluclateFollFee()} SEK</>
    </Container>
  );
};

export default Home;
