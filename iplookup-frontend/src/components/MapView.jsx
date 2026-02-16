function MapView({ latitude, longitude }) {
  if (!latitude || !longitude) return null;

  return (
    <div style={{ marginTop: "20px" }}>
      <iframe
        width="100%"
        height="400"
        style={{ border: 0, borderRadius: "12px" }}
        loading="lazy"
        allowFullScreen
        src={`https://www.google.com/maps?q=${latitude},${longitude}&z=12&output=embed`}
      ></iframe>
    </div>
  );
}

export default MapView;
