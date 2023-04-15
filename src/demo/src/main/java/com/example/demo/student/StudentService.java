package com.example.demo.student;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class StudentService {

    private final StudentRepository studentRepository;
    @Autowired
    public StudentService(StudentRepository studentRepository){

        this.studentRepository = studentRepository;
    }

    public Student registerStudent(Student student){
        Optional<Student> studentOptional = studentRepository
                .studentExists(student.getEmail());

        if(studentOptional.isPresent()){
            throw new IllegalStateException("student with this email already exists");
        }

        studentRepository.save(student);
        return student;
    }

    public List<Student> getStudents(){
        return studentRepository.findAll();
    }
}
