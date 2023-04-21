import { useState } from 'react';
import ReactMapGL, { Source, Layer } from 'react-map-gl';

function MapComponent() {
  const [viewport, setViewport] = useState({
    width: '100%',
    height: '400px',
    latitude: 37.7577,
    longitude: -122.4376,
    zoom: 8,
  });

  const data = {
    type: 'FeatureCollection',
    features: [
      {
        type: 'Feature',
        geometry: {
          type: 'Point',
          coordinates: [-122.414, 37.776],
        },
        properties: {
          id: '1',
          name: 'Marker 1',
        },
      },
      {
        type: 'Feature',
        geometry: {
          type: 'Point',
          coordinates: [-122.416, 37.774],
        },
        properties: {
          id: '2',
          name: 'Marker 2',
        },
      },
      // Add more features here...
    ],
  };

  return (
    <ReactMapGL
      {...viewport}
      mapboxAccessToken={process.env.NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN}
      onViewportChange={(nextViewport) => setViewport(nextViewport)}
    >
      <Source id="markers" type="geojson" data={data}>
        <Layer
          id="marker-layer"
          type="circle"
          paint={{
            'circle-color': '#4264fb',
            'circle-radius': 6,
          }}
        />
      </Source>
    </ReactMapGL>
  );
}
