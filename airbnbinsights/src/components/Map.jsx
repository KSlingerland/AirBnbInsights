import React, { useRef, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import 'mapbox-gl/dist/mapbox-gl.css';
import mapboxgl from '!mapbox-gl'; // eslint-disable-line import/no-webpack-loader-syntax
mapboxgl.accessToken = 'pk.eyJ1Ijoia3NsaW5nZXJsYW5kIiwiYSI6ImNsajQ0dmI0NjBheWYzZW1qMjJ1ZWZwbXQifQ.Bkgu770BSa64RJvRjY1JfA';

const Map = () => {
    const navigate = useNavigate();

  const mapContainer = useRef(null);
  const map = useRef(null);
  const [lng, setLng] = useState(0.1268);
  const [lat, setLat] = useState(51.5045);
  const [zoom, setZoom] = useState(9);


  useEffect(() => {
    if (map.current) return; // initialize map only once
    map.current = new mapboxgl.Map({
      container: 'map',
      style: 'mapbox://styles/mapbox/streets-v12',
      center: [lng, lat],
      zoom: zoom
    });

    map.current.addControl(new mapboxgl.NavigationControl(), "top-right");

    map.current.on('load', () => {
        map.current.resize();
      map.current.addSource('neighbourhoods', {
        type: 'geojson',
        data: './neighbourhoods.geojson'
      })

      map.current.addSource('listings', {
        type: "geojson",
        data: "https://localhost:7158/api/listings"
      })

      map.current.addLayer({
        'id': 'neighbourhood-layer',
        'type': 'line',
        'source': 'neighbourhoods',
      });

      map.current.addLayer({
        id: 'listings-layer',
        type: 'circle',
        source: 'listings'
      })
      // Create a popup, but don't add it to the map yet.
const popup = new mapboxgl.Popup({
  closeButton: false
  });

      // When a click event occurs on a feature in the places layer, open a popup at the
    // location of the feature, with description HTML from its properties.
    map.current.on('mousemove', 'listings-layer', (e) => {
      // Change the cursor style as a UI indicator.
      map.current.getCanvas().style.cursor = 'pointer';
      // Copy coordinates array.
      const coordinates = e.features[0].geometry.coordinates.slice();
      const title = e.features[0].properties.name;
      const hostName = e.features[0].properties.hostname
      const price = e.features[0].properties.price
      const id = e.features[0].properties.id
      
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
          "<p>" + title + id +"</p>" +
          "<p>" + hostName + "</p>" +
          "<strong><p>" + price + "</p></strong>" +
        "</div>" 
      )
      .addTo(map.current);
    });

    map.current.on('mouseleave', 'listings-layer', () => {
      map.current.getCanvas().style.cursor = '';
      popup.remove();
      });

      map.current.on('click', 'listings-layer', (e) => {
        console.log(e)
        navigate(`/listing/${e.features[0].properties.id}`)
      })
      
      // Change the cursor to a pointer when the mouse is over the places layer.
      map.current.on('mouseenter', 'places', () => {
      map.current.getCanvas().style.cursor = 'pointer';
      });
      
      // Change it back to a pointer when it leaves.
      map.current.on('mouseleave', 'places', () => {
      map.current.getCanvas().style.cursor = '';
      });
    })
  }, []);

  const markerClicked = (title) => {
    window.alert(title);
  };

  useEffect(() => {
    if (!map.current) return; // wait for map to initialize
    map.current.on('move', () => {
      setLng(map.current.getCenter().lng.toFixed(4));
      setLat(map.current.getCenter().lat.toFixed(4));
      setZoom(map.current.getZoom().toFixed(2));
    });

    console.log(lng, lat, zoom)
  });

  return (
      <div ref={mapContainer} className="map-container" id='map'/>
  );
}

export default Map;