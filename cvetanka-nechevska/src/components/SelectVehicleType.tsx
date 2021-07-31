import React from "react";

import { FormControl, InputLabel, Select, styled } from "@material-ui/core";
import { MENU_ITEMS } from "../constants/constants";

const SelectFormControl = styled(FormControl)({
  width: "120px",
  marginTop: "30px",
});

const SelectVehicleType = (props: {
  vechileType: string;
  handleChange: (arg: any) => void;
}) => {
  return (
    <SelectFormControl>
      <InputLabel>Vehicle Type</InputLabel>
      <Select
        native
        fullWidth
        value={props.vechileType}
        onChange={props.handleChange}
      >
        {MENU_ITEMS.map((item) => (
          <option value={item}>{item}</option>
        ))}
      </Select>
    </SelectFormControl>
  );
};

export default SelectVehicleType;
