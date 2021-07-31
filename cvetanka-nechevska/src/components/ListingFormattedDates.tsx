import React from "react";

import { Box, Chip } from "@material-ui/core";

const ListingFormattedDates = (props: {
  dates: Date[];
  handleDeleteDate: (arg: any) => void;
}) => {
  return (
    <Box display="flex" flexDirection="row" flexWrap="wrap" alignItems="center" p={2}>
      {props.dates.length > 0 &&
        props.dates.map((date: Date) => (
          <Box display="flex" flexDirection="row" p={2}>
            <Chip
              label={date}
              onDelete={() => props.handleDeleteDate(date)}
              color="primary"
            />
          </Box>
        ))}
    </Box>
  );
};

export default ListingFormattedDates;
