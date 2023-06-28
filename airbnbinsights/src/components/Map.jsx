import React, { useRef, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import 'mapbox-gl/dist/mapbox-gl.css';
import mapboxgl from '!mapbox-gl'; // eslint-disable-line import/no-webpack-loader-syntax

mapboxgl.accessToken = 'pk.eyJ1Ijoia3NsaW5nZXJsYW5kIiwiYSI6ImNsajQ0dmI0NjBheWYzZW1qMjJ1ZWZwbXQifQ.Bkgu770BSa64RJvRjY1JfA';

const Map = () => {
  const navigate = useNavigate();

  const mapContainer = useRef(null);
  const [map, setMap] = useState(null);
  const [lng, setLng] = useState(0.1268);
  const [lat, setLat] = useState(51.5045);
  const [zoom, setZoom] = useState(9);

  const [filters, setFilters] = useState({
    category: '',
    dateRange: '',
    // Add more filter properties as needed
  });

  useEffect(() => {
    if (map) return; // initialize map only once

    const initializeMap = ({ setMap, mapContainer }) => {
      const map = new mapboxgl.Map({
        container: mapContainer.current,
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [lng, lat],
        zoom: zoom
      });

      map.addControl(new mapboxgl.NavigationControl(), "top-right");
  
      map.on('load', () => {
        setMap(map);
        map.resize();

        map.addSource('neighbourhoods', {
          type: 'geojson',
          data: '/./neighbourhoods.geojson'
        })
    
        map.addSource('listings', {
          type: "geojson",
          data: `${process.env.REACT_APP_API}/listings`
        })

        map.addLayer({
          'id': 'neighbourhood-layer',
          'type': 'line',
          'source': 'neighbourhoods',
        });
    
        map.addLayer({
          id: 'listings-layer',
          type: 'circle',
          source: 'listings'
        })
      });
    };
    if (!map) initializeMap({ setMap, mapContainer });
    // eslint-disable-next-line
  }, [map]);

  useEffect(() => {
    if (!map) return;
    // Create a popup, but don't add it to the map yet.
    const popup = new mapboxgl.Popup({
      closeButton: false
      });

    // When a click event occurs on a feature in the places layer, open a popup at the
    // location of the feature, with description HTML from its properties.
    map.on('mousemove', 'listings-layer', (e) => {
      // Change the cursor style as a UI indicator.
      map.getCanvas().style.cursor = 'pointer';
      // Copy coordinates array.
      const coordinates = e.features[0].geometry.coordinates.slice();
      const id = e.features[0].properties.id;
      
      // Ensure that if the map is zoomed out such that multiple
      // copies of the feature are visible, the popup appears
      // over the copy being pointed to.
      while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
      coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
      }
    
      popup
      .setLngLat(coordinates)
      .setHTML(
        "<div>" + 
          "<p>" + id +"</p>" +
        "</div>" 
      )
      .addTo(map);
    });

  map.on('mouseleave', 'listings-layer', () => {
    map.getCanvas().style.cursor = '';
    popup.remove();
    });

    map.on('click', 'listings-layer', (e) => {
      console.log(e)
      navigate(`/listing/${e.features[0].properties.id}`)
    })
    
    // Change the cursor to a pointer when the mouse is over the places layer.
    map.on('mouseenter', 'places', () => {
    map.getCanvas().style.cursor = 'pointer';
    });
    
    // Change it back to a pointer when it leaves.
    map.on('mouseleave', 'places', () => {
    map.getCanvas().style.cursor = '';
    });
  })

  useEffect(() => {
    if (!map) return; // wait for map to initialize
    map.on('move', () => {
      setLng(map.getCenter().lng.toFixed(4));
      setLat(map.getCenter().lat.toFixed(4));
      setZoom(map.getZoom().toFixed(2));
    });

    console.log(lng, lat, zoom)
  });

  useEffect(() => {
    if (!map) return; 
    // Apply filters whenever they change
    if (map) {
      const filteredData = filterData(); // Implement your filtering logic here based on the filter state
      map.getSource('listings').setData(filteredData)
    }
    // eslint-disable-next-line
  }, [filters]);

  const filterData = () => {
    const { category } = filters;

    // Filter based on category
    let filteredData = map.querySourceFeatures('listings');
    console.log(filteredData)

    if (category) {
      filteredData = filteredData.filter(item => item.properties.neighbourhood === category);
      console.log(filteredData)
    }
    // Return the filtered data as a GeoJSON object
    return {
      type: 'FeatureCollection',
      features: filteredData.map(item => ({
        type: 'Feature',
        geometry: {
          type: 'Point',
          coordinates: [item.geometry.coordinates[1], item.geometry.coordinates[0]],
        },
        properties: {

        },
    })),
    }
  };

  const handleCategoryChange = (event) => {
    setFilters({ ...filters, category: event.target.value });
  };

  return (
    <>
      <div ref={mapContainer} className="map-container" id='map'/>
      <div>
        <label>
          Neighbourhood:
          <select value={filters.category} onChange={handleCategoryChange}>
            <option value="">All</option>
            <option value="Westminster">Westminster</option>
            <option value="Greenwich">Category 2</option>
            {/* Add more options as needed */}
          </select>
        </label>
      </div>
    </>
  );
}

export default Map;