import React, { useEffect, useState } from "react";
import { Chart } from "react-charts";

const HostsBar = () => {
  const [chartData, setChartData] = useState()

  useEffect(() => {
    const fetchChart = async () => {
        const response = await fetch(`https://localhost:7158/api/charts/neighbourhoods`)
        setChartData(response)
    }
    console.log(chartData)
    fetchChart()
}, [])

  var data = [
    {
      label: "Neibourhoods",
      data: [
        {
          primary: "Neibourhoods",
          secondary: 33
        }
      ]
    },
    {
      label: "Hosts",
      data: [
        {
          primary: "Amount of Hosts",
          secondary: 47619
        }
      ]
    }
  ]


  const primaryAxis = React.useMemo(
    () => ({
      getValue: datum => datum.primary,
    }),
    []
  )
  const secondaryAxes = React.useMemo(
    () => [
      {
        getValue: datum => datum.secondary,
      },
    ],
    []
  )

  return (
    <>
      <br />
      <br />
        <Chart
          options={{
            data,
            primaryAxis,
            secondaryAxes,
          }}
        />
    </>
  );
}

export default HostsBar;