import express from 'express';
import Joi from 'joi';

import TaxServiceImpl from "./services/tax";
import VehicleFactoryImpl from "./factories/vehicle";
import { TollFreeVehicles, TollVehicles } from './models/types';

const app = express();

const port = 3000;

app.use(express.json());

app.post('/tax', (req, res) => {
  const validationSchema = Joi.object({
    dates: Joi.array().items(Joi.string()).required(),
    vehicleType: Joi.string().valid(...[...Object.values(TollVehicles), ...Object.values(TollFreeVehicles)]).required()
  });

  const { error, value: { dates, vehicleType } } = validationSchema.validate(req.body);

  if (error) {
    return res.status(400).json({
      message: 'Invalid body object',
      errors: error.details.map(err => err.message),
    });
  }

  const taxService = TaxServiceImpl.getInstance(new VehicleFactoryImpl());

  const amount = taxService.getTax(vehicleType, dates);

    const taxAmount = {
      amount,
    };

    res.json(taxAmount);
})

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`);
})