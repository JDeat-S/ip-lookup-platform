import { useState } from "react";
import { queryIp } from "../services/api";
const isValidIP = (ip) => {
  const ipv4 =
    /^(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}$/;

  const ipv6 =
    /^(([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}|::1)$/;

  return ipv4.test(ip) || ipv6.test(ip);
};

function IpForm({ onSuccess }) {
  const [ip, setIp] = useState("");

  const handleSubmit = async (e) => {
  e.preventDefault();

  if (!isValidIP(ip)) {
    alert("Formato de IP inválido.");
    return;
  }

  try {
    await queryIp(ip);
    setIp("");
    onSuccess();
  } catch (error) {
    if (error.response?.status === 409) {
      alert("La IP ya fue consultada.");
      onSuccess(); 
    } else {
      alert("Error al consultar IP.");
    }
  }
};

return (
  <div className="card shadow-sm mb-4">
    <div className="card-body">
      <form onSubmit={handleSubmit} className="row g-2">
        <div className="col-md-10">
          <input
            type="text"
            className="form-control"
            placeholder="Ingresa una IP (Ej: 8.8.8.8)"
            value={ip}
            onChange={(e) => setIp(e.target.value)}
            required
          />
        </div>
        <div className="col-md-2">
          <button type="submit" className="btn btn-dark w-100">
            Consultar
          </button>
        </div>
      </form>
    </div>
  </div>
);

}

export default IpForm;
