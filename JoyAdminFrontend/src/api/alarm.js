import { axios } from '@/libs/api.request'
// Curl curl -X 'POST' \ 'http://localhost:9001/api/AlarmHistory/GetAlarmHistories' \
// -H 'accept: text/plain' \
// -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY4NjIwNzAzNiwibmJmIjoxNjg2MjA3MDM2LCJleHAiOjE2ODY0NjYyMzYsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.Y8U5-H7LC0fg7F3A0cBLyk-8b85G9MhgHPJHBx_Zch8' \
// -H 'Content-Type: application/json' \
// -H 'request-from: swagger' \
// -d '{ "Station": "string", "Start": "2023-06-09T05:47:53.668Z", "End": "2023-06-09T05:47:53.668Z", "page": 0, "size": 0 }'
export const GetAlarmHistories = (params) => {
  return axios.post('api/AlarmHistory/GetAlarmHistories', {
    ...params
  })
}

// curl -X 'POST' \
//   'http://localhost:9001/api/AlarmHistory/GetAlarmFreq' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY4NjIwNzAzNiwibmJmIjoxNjg2MjA3MDM2LCJleHAiOjE2ODY0NjYyMzYsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.Y8U5-H7LC0fg7F3A0cBLyk-8b85G9MhgHPJHBx_Zch8' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Station": "string",
//   "Start": "2023-06-09T05:50:45.997Z",
//   "End": "2023-06-09T05:50:45.997Z",
//   "TopN": 0,
//   "OrderMode": 0
// }'

export const GetAlarmFreq = (params) => {
  return axios.get('api/AlarmHistory/GetAlarmFreq', {
    params
  })
}

// curl -X 'POST' \
//   'http://localhost:9001/api/AlarmHistory/GetAlarmCount' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY4NjIwNzAzNiwibmJmIjoxNjg2MjA3MDM2LCJleHAiOjE2ODY0NjYyMzYsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.Y8U5-H7LC0fg7F3A0cBLyk-8b85G9MhgHPJHBx_Zch8' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Station": "string",
//   "Start": "2023-06-09T05:53:08.245Z",
//   "End": "2023-06-09T05:53:08.245Z"
// }'

export const GetAlarmCount = (params) => {
  return axios.get('api/AlarmHistory/GetAlarmCount', {
    params
  })
}

// curl -X 'POST' \
//   'http://localhost:9001/api/AlarmHistory/PostAlarm' \
//   -H 'accept: */*' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTY4NjIwNzAzNiwibmJmIjoxNjg2MjA3MDM2LCJleHAiOjE2ODY0NjYyMzYsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.Y8U5-H7LC0fg7F3A0cBLyk-8b85G9MhgHPJHBx_Zch8' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Station": "string",
//   "Address": "string",
//   "Message": "string",
//   "StartTime": "2023-06-09T05:53:53.329Z",
//   "EndTime": "2023-06-09T05:53:53.329Z"
// }'

export const PostAlarm = (params) => {
  return axios.post('api/AlarmHistory/PostAlarm', params)
}
export default { PostAlarm, GetAlarmCount, GetAlarmFreq, GetAlarmHistories }
