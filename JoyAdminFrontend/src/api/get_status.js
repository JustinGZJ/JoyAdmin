import Axios from 'axios'

const baseUrl = process.env.NODE_ENV === 'development' ? 'http://36.134.23.53:11880' : 'http://192.168.1.180:1880'
const axios = Axios.create({
  baseURL: baseUrl,
  timeout: 120000
})

export const getByKey = (key) => {
  return axios.get(`/map/${key}`)
}

// eslint-disable-next-line camelcase
export const getAlarms = (station) => {
  if (station === 'all') return axios.get(`/alarm`)
  else {
    return axios.get(`/alarm/${station}`)
  }
}
export const getStatus = (station) => {
  if (station === 'all') return axios.get(`/status`)
  else {
    return axios.get(`/status/${station}`)
  }
}

export const groupValuesByStation = (data) => {
  const valuesByStation = {}

  Object.keys(data).forEach(key => {
    const [, station, , prop] = key.split('.')

    if (!valuesByStation[station]) {
      valuesByStation[station] = {}
    }
    valuesByStation[station][prop] = data[key].value.value || '-'
  })
  return valuesByStation
}
export default { getStatus, getByKey, getAlarms, groupValuesByStation }
