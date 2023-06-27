import * as React from 'react';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import { Rating, Slider } from '@mui/material';
import StarIcon from '@mui/icons-material/Star';

const Filter = () => {
    const [age, setAge] = React.useState('');
    const [value, setValue] = React.useState(0);
    const [hover, setHover] = React.useState(-1);
    const [slider, setSlider] = React.useState([20, 200]);
    
    const handleChange = (event) => {
    setAge(event.target.value);
  };

  const handleSlider = (event, newValue) => {
    setSlider(newValue);
  }

  const labels = {
    0.5: 'Useless',
    1: 'Useless+',
    1.5: 'Poor',
    2: 'Poor+',
    2.5: 'Ok',
    3: 'Ok+',
    3.5: 'Good',
    4: 'Good+',
    4.5: 'Excellent',
    5: 'Excellent+',
  };

  function getLabelText(value) {
    return `${value} Star${value !== 1 ? 's' : ''}, ${labels[value]}`;
  }

  function valuetext(value) {
    return `${value}°$`;
  }
  

    return (
        <Box sx={{}}>
            <FormControl>
                <InputLabel id="demo-simple-select-label">Neighbourhood</InputLabel>
                <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={age}
                label="Neighbourhoods"
                onChange={handleChange}
                >
                    <MenuItem value={"Brent"}>Brent</MenuItem>
                </Select>
            </FormControl>

            <Slider
                value={slider}
                onChange={handleSlider}
                getAriaLabel={() => "Price range"}
                getAriaValueText={valuetext}
                min={0}
                max={500}
            />

            <Rating
                name="hover-feedback"
                value={value}
                precision={0.5}
                getLabelText={getLabelText}
                onChange={(event, newValue) => {
                setValue(newValue);
                }}
                onChangeActive={(event, newHover) => {
                setHover(newHover);
                }}
                emptyIcon={<StarIcon style={{ opacity: 0.55 }} fontSize="inherit" />}
            />
            {value !== null && (
                <Box sx={{ ml: 2 }}>{labels[hover !== -1 ? hover : value]}</Box>
            )}
        </Box>
    )
}

export default Filter