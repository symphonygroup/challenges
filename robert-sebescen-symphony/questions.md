# Questions
1. Found the "Tractor" vehicle type in the code. It is not mentioned anywhere in the assignment so I have removed it from the code until clarified. Should we treat it as taxable or not?

2. Please confirm the list of supported holidays:
- New Year's Day
- Epiphany
- Good Friday
- Easter
- Easter Monday
- Internationl Workers' Day (Labour Day)
- Ascension Day
- Whit Sunday
- National Day of Sweden
- Midsummer's Day
- All Saints' Day
- Christmas Day
- Second Day of Christmas (Boxing Day)

# Questions before proceeding the bonus scenario
In order to abstract the tax calculation I will need more details on how it is going to be different for other cities.
If there are other rules like
`Maximum weekly charge is X` or `Tax is not charged on the last Friday of the Month` the existing alghoryrm will need to change. Or if the charges are not fixed per hours but change based on some logic.

For the purpose of the challenge I will proceed with assumption that following rules are either applied or not
- Maximum amount per day
- Non charged week days
- Non charged months
- Public holidays are not charged
- Day before holiday is not charged
- Tax exempt vehicles
- Single charge rules
Also, the assumption is that the congestion tax is charged during fixed hours (and fixed for each vehicle)


