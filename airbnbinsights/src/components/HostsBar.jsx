import React from "react";
import { Chart } from "react-charts";
import placeholder from '../data/dashboard.json'

const HostsBar = () => {

  // const { isLoading, error, data } = useQuery(['repoData'], () =>
  //   api.dashboard.get(),
  //   {
  //     select: (data) => console.log(data)
  //   }
  // )

  const data = placeholder

  const primaryAxis = React.useMemo(
    () => ({
      getValue: (datum) => datum.primary,
    }),
    []
  );

  const secondaryAxes = React.useMemo(
  () => [
    {
      getValue: (datum) => datum.secondary,
      elementType: 'bar',
    },
  ],
  []
);

  // if (isLoading) return 'Loading...'

  // if (error) return 'An error has occurred: ' + error.message

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