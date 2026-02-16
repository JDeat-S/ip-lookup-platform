import MapView from "./MapView";
import { useEffect, useState } from "react";
import { getAllIps, deleteIp } from "../services/api";
const isValidIP = (ip) => {
  const ipv4 =
    /^(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}$/;

  const ipv6 =
    /^(([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}|::1)$/;

  return ipv4.test(ip) || ipv6.test(ip);
};

function IpTable({ refreshTrigger }) {
  const [data, setData] = useState([]);
  const [filter, setFilter] = useState("");
const [selectedIp, setSelectedIp] = useState(null);

  const fetchData = async () => {
    const response = await getAllIps();
    setData(response.data);
  };

useEffect(() => {
  fetchData();
}, [refreshTrigger]);


  const handleDelete = async (id) => {
    await deleteIp(id);
    fetchData();
  };

  const filteredData = data.filter((item) =>
    item.ipAddress.toLowerCase().includes(filter.toLowerCase())
  );
const getThreatBadge = (level) => {
  switch (level?.toLowerCase()) {
    case "low":
      return "bg-success";
    case "medium":
      return "bg-warning text-dark";
    case "high":
      return "bg-danger";
    default:
      return "bg-secondary";
  }
};

return (
  <div className="card shadow-sm mb-4">
    <div className="card-body">

      <div className="mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Filtrar por IP"
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>

      <div className="table-responsive">
        <table className="table table-hover align-middle">
          <thead className="table-dark">
            <tr>
              <th>IP</th>
              <th>Country</th>
              <th>Ciudad</th>
              <th>ISP</th>
              <th>Lat</th>
              <th>Lng</th>
              <th>Threat</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {filteredData.map((item) => (
              <tr
                key={item.id}
                style={{ cursor: "pointer" }}
                onClick={() => setSelectedIp(item)}
              >
                <td>{item.ipAddress}</td>
                <td>{item.country}</td>
                <td>{item.city}</td>
                <td>{item.isp}</td>
                <td>{item.latitude}</td>
                <td>{item.longitude}</td>
                <td>
                  <span className={`badge ${getThreatBadge(item.threatLevel)}`}>
                    {item.threatLevel}
                  </span>
                </td>
                <td>
                  <button
                    className="btn btn-sm btn-outline-danger"
                    onClick={(e) => {
                      e.stopPropagation();
                      handleDelete(item.id);
                    }}
                  >
                    Eliminar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {selectedIp && (
        <div className="mt-4">
          <h5>Ubicación de {selectedIp.ipAddress}</h5>
          <MapView
            latitude={selectedIp.latitude}
            longitude={selectedIp.longitude}
          />
        </div>
      )}

    </div>
  </div>
);
}

export default IpTable;
