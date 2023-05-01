import axios from 'axios'
import { baseUrl } from '../constants/constants'

export const getAllBlogs = () => axios.get(`${baseUrl}/blogs`)

export const getTopBlogs = () => axios.get(`${baseUrl}/blogs/top`)

export const getBlog = (id) => axios.get(`${baseUrl}/blogs/article/${id}`)

export const addLike = (id) => axios.put(`${baseUrl}/blogs/like/${id}`)
