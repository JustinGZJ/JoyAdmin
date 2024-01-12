import { axios } from '@/libs/api.request'
// curl -X 'GET' \
//   'http://127.0.0.1:9001/api/ProcessControl/GetProcessRecordByBarCode/222222' \
//   -H 'accept: text/plain' \
//   -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEsIkFjY291bnQiOiJhZG1pbiIsIk5hbWUiOm51bGwsImlhdCI6MTcwMDYxOTU0MiwibmJmIjoxNzAwNjE5NTQyLCJleHAiOjE3MDA4Nzg3NDIsImlzcyI6ImRvdG5ldGNoaW5hIiwiYXVkIjoicG93ZXJieSBGdXJpb24ifQ.5ALXPysA71zNI6tKivEkhxuBPc1cetapLdDkzufDraE' \
//   -H 'request-from: swagger'
export const GetProcessRecordByBarCode = (code) => {
  return axios.get('api/ProcessControl/GetProcessRecordByBarCode/' + code)
}
